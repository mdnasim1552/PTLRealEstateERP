<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ASITNEW.Master" CodeBehind="HRHolidays.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HRHolidays" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .modal-lg {
            width: 80%;
        }
        .custTable tr{
             background:#f0f6fa;
        }
        .custTable tr td{
            padding:5px 5px;
        }
        .custTable tr td input{
            display:none;
        }
        .custTable tr td label{
            margin:0 5px;
            display:block;
            padding:0 12px;
           
        }
         .custTable tr:hover{
             background:#5b9bd1;
            cursor:pointer;
        }
          .custTable tr td:hover, .custTable tr td label:hover{
          cursor:pointer;
          color:#fff;
           
        }
         


          
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", ".classAdd", function () {

               
                var rowCount = $('.data-holiday').length + 1;
                var holidaydiv = '<tr class="data-holiday">' +
                    '<td> <select name="htype' + rowCount + '" id="htype" class="form-control htype01"> ' +
                    '< option value = "" > Select Holidays </option>' +

                    '<option value="W">Weekend Day</option>' +
                    '<option value="H">Govt.Holi Day</option>' +
                    '<option value="ST">Special Thursday Day</option>' +

                    '</select ></td>' +

                    '<td><input type="date" name="h-date' + rowCount + '" class="form-control hdate01" /></td>' +

                    '<td><input type="text" name="h-occasion' + rowCount + '" class="form-control hoccasion01" /></td>' +
                    '<td><button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd">Add More</button>' +
                    '<button type="button" id="btnDelete" class="deleteHoliday btn btn btn-danger btn-xs">Remove</button></td>' +
                    '</tr>';
                $('#maintable').append(holidaydiv);
            });


        });

        $(document).on("click", ".deleteHoliday", function () {
            $(this).closest("tr").remove();
        });





        function getAllHolidayData() {
            var data = [];
            $('tr.data-holiday').each(function () {
                var holidayType = $(this).find('.htype01').val();
                var HolidayDate = $(this).find('.hdate01').val();
                var Occasion = $(this).find('.hoccasion01').val();
                var alldata = {
                    'holidayType': holidayType,
                    'HolidayDate': HolidayDate,
                    'Occasion': Occasion
                }
                data.push(alldata);
            });
            console.log(data);//  
            return data;
        }

        $(document).on("click", "#btnSubmit", function () {

            //$("#btnSubmit").click(function () {
            var data = JSON.stringify(getAllHolidayData());
            console.log(data);
            $.ajax({

                url: '<%=ResolveClientUrl("~/Service/UserService.asmx/SaveHolidayData")%>',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ 'holidaydata': data }),
                success: function () {
                    alert("Data Added Successfully");
                },
                error: function () {
                    alert("Error while inserting data");
                }
            });
        });

        $(document).on("change", '.hdate01', function () {
            var $this = $(this),
                date = $this.val();

            var valueArray = $('.hdate01').map(function () {
                return this.value;
            }).get();

            //console.log(date);
            //console.log(valueArray);

            var result = valueArray.slice(0, -1);

            /*  console.log(result);*/

            if (result.indexOf(date) !== -1) {
                alert("Yes, Date Already  exists!")
                $this.val('');
            }



        });

        function arrayRemove(arr, value) {

            return arr.filter(function (ele) {
                return ele != value;
            });
        }



        function loadModal() {
            $('#addHolidayModal').modal('toggle', {
                backdrop: 'static',
                keyboard: false

            });

        }




        function closeModal() {
            $('#addHolidayModal').modal('hide');
        }


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="card card-fluid container-data" style="min-height: 1000px;">
                <div class="card-header">
                    <div class="row">

                        <div class="col-md-3 offset-md-3">
                            <%--<a class="btn btn-success" href="javascript:;">Mark All Friday Holiday
                                <i class="fa fa-check"></i></a>--%>

                            <asp:LinkButton ID="markallfriday" runat="server" class="btn btn-success" OnClick="markallfriday_Click">  <i class="fa fa-check"></i>Generate All Friday Holiday(Year)</asp:LinkButton>


                        </div>
                        <div class="col-md-2 offset-md-4">
                            <button type="button" id="addHoliday" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#addHolidayModal" onclientclick="loadModal();"><i class="fa fa-plus"></i> Add Holiday </button>
                        </div>


                    </div>
                </div>
                <div class="card=body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rblmonth" runat="server" AutoPostBack="true" 
                                CssClass="table custTable"
                                OnSelectedIndexChanged="rblmonth_SelectedIndexChanged">
                                <asp:ListItem Value="01" Selected="True"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; January</asp:ListItem>
                                <asp:ListItem Value="02"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; February</asp:ListItem>
                                <asp:ListItem Value="03"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; March</asp:ListItem>
                                <asp:ListItem Value="04"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; April</asp:ListItem>
                                <asp:ListItem Value="05"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; May</asp:ListItem>
                                <asp:ListItem Value="06"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; June</asp:ListItem>
                                <asp:ListItem Value="07"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; July</asp:ListItem>
                                <asp:ListItem Value="08"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; August</asp:ListItem>
                                <asp:ListItem Value="09"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; September</asp:ListItem>
                                <asp:ListItem Value="10"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; October</asp:ListItem>
                                <asp:ListItem Value="11"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; November</asp:ListItem>
                                <asp:ListItem Value="12"><span class="fa fa-sm fa-calendar" aria-hidden="true" ></span>&nbsp; December</asp:ListItem>
                            </asp:RadioButtonList>

                            <%--          
            <input type="radio" class="btn-check" name="options-outlined" id="success-outlined" autocomplete="off" checked>
            <label class="btn btn-outline-success" for="success-outlined">Checked success radio</label>--%>
                        </div>
                        <div class="col-md-10">

                            <asp:Label ID="lblmonth" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>

                            <asp:GridView ID="gvholiday" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True"
                                 OnRowDataBound="gvholiday_RowDataBound">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEmpName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hdate")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Occaasion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOccasion" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hoccasion")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvhdaytype" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hdaytype")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdayname" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hdayname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="LnkbtnDelete" CssClass="btn btn-sm btn-danger"
                                                OnClick="LnkbtnDelete_Click">
                                                <span class="fa fa-sm fa-trash" aria-hidden="true" ></span>&nbsp;
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>

                                    <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Button" CancelText="" />--%>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>




                            <div id="addHolidayModal" class="modal  fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content col-md-12 col-sm-12 ">
                                        <div class="modal-header hedcon">

                                            <h4>Holiday Entry</h4>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>

                                        </div>
                                        <div class="modal-body">
                                            <table class="table" id="maintable">
                                                <thead>
                                                    <tr>
                                                        <th>Type</th>
                                                        <th>Date</th>
                                                        <th>Occasion</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr class="data-holiday">
                                                        <td>

                                                            <select name="htype" id="htype" class="form-control htype01">
                                                                <option value="">Select Holidays </option>

                                                                <option value="W">Weekend Day</option>
                                                                <option value="H">Govt.Holi Day</option>
                                                                <option value="ST">Special Thursday Day</option>

                                                            </select>



                                                            <%-- <input type="text" name="f-name" class="form-control f-name01" /></td>--%>
                                                        <td>
                                                            <input type="date" name="h-date" class="form-control hdate01" /></td>
                                                        <td>
                                                            <input type="text" name="h-occasion" class="form-control hoccasion01" /></td>
                                                        <td>
                                                            <button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd">Add More</button>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                            <button type="button" id="btnSubmit" class="btn btn-primary btn-md pull-right btn-sm">Submit</button>
                                        </div>


                                    </div>
                                </div>
                            </div>








                        </div>





                    </div>


                </div>



            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
