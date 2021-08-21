<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PrjCompFlowchart.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.PrjCompFlowchart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .RowColor {
            color: maroon;
            font-size: 14px !important;
            font-family: Cambria;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function loadModal() {
            $('#AddComments').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }


        function UsrloadModal() {
            $('#AddUser').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function DocloadModal() {
            $('#AddDoc').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }


        function CloseModal() {
            $('#AddComments').modal('hide');
            $('#AddUser').modal('hide');
            $('#AddDoc').modal('hide');
        }


        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>


    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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
    <div class="card card-fluid" style="min-height: 450px;">
        <div class="card-body">
            <div class="row">

                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label" for="ddlUserName">Project Name</label>
                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="custom-select chzn-select">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label" for="ddlUserName">Type</label>
                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="custom-select chzn-select">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="margin-top30px btn btn-primary  " OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                    </div>
                </div>

                <div class="col-md-3 margin-top30px">
                    <label id="chkbod" runat="server" class="switch">
                        <asp:CheckBox ID="Chboxchild" runat="server" AutoPostBack="true" OnCheckedChanged="Chboxchild_CheckedChanged" />
                        <span class="btn btn-xs slider round"></span>
                    </label>
                    <asp:Label ID="lblchild" runat="server" Text="Show All" CssClass="btn btn-xs"></asp:Label>
                </div>

                <div class="col-md-2" style="display: none;">
                    <div class="form-group">
                        <label class="control-label">Approval Date</label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                    </div>

                </div>


            </div>

            <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                ShowFooter="True" Width="668px" Style="margin-right: 0px" OnSelectedIndexChanged="gvPrjInfo_SelectedIndexChanged" OnRowCreated="gvPrjInfo_RowCreated" OnRowDataBound="gvPrjInfo_RowDataBound">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="+">

                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnAddUsr" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New User" BackColor="Transparent" Visible="false" OnClick="lbtnAddUsr_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>


                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assign </br>User">
                        <ItemTemplate>
                            <asp:Label ID="lblgvAusrn" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assusrsname").ToString())%>'
                                Width="50px" Font-Size="10px"></asp:Label>
                        </ItemTemplate>


                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ActCode" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lgvActcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                Width="200px" Font-Size="11px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Deptcode" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lgvdeptcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcod")) %>'
                                Width="200px" Font-Size="11px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Activities">
                        <ItemTemplate>
                            <asp:Label ID="lgvActivi" runat="server"
                                Text='<%# "<B>"+ "<span class=RowColor>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc"))+"</span>" + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                Width="300px" Font-Size="9px">
                                            
                                            
                                              ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnCalculaton" runat="server" CssClass="btn  btn-primary primarygrdBtn" OnClick="lbtnCalculaton_Click">Calculation</asp:LinkButton>

                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description " Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvRespon" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "responsibility").ToString())%>'
                                Width="125px" Font-Size="9px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="lUpdatPerInfo_Click"
                                CssClass="btn btn-danger primaryBtn">Save</asp:LinkButton>
                        </FooterTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start</br>Date">

                        <ItemTemplate>
                            <asp:TextBox ID="txtTarsDate" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tarsdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tarsdat")).ToString("dd-MMM-yyyy")%>'
                                Width="65px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttarsDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttarsDate"></cc1:CalendarExtender>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End</br>Date">

                        <ItemTemplate>
                            <asp:TextBox ID="txttarEndDate" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "taredat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "taredat")).ToString("dd-MMM-yyyy")%>'
                                Width="65px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttarEndDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttarEndDate"></cc1:CalendarExtender>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dura</br>tion">

                        <FooterTemplate>
                            <asp:Label ID="lblgvFTotal" runat="server"></asp:Label>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvduration" runat="server" BackColor="Transparent" ReadOnly="true"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;-#,##0; ")%>'
                                Width="20px" Style="text-align: right"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="btnTotal" runat="server"
                                CssClass="">Total</asp:Label>
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Budgeted</br>Amount">

                        <FooterTemplate>
                            <asp:Label ID="lblgvFbgdamt" runat="server"></asp:Label>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvbgdamt" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;-#,##0; ")%>'
                                Width="60px" Style="text-align: right"></asp:TextBox>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start</br>Date">

                        <ItemTemplate>
                            <asp:TextBox ID="txtAcStDate" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acstdat")).ToString("dd-MMM-yyyy")%>'
                                Width="65px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtAcStDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtAcStDate"></cc1:CalendarExtender>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End</br>Date">

                        <ItemTemplate>
                            <asp:TextBox ID="txtAcEndDate" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "acenddat")).ToString("dd-MMM-yyyy")%>'
                                Width="65px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtAcEndDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtAcEndDate"></cc1:CalendarExtender>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dura</br>tion">

                        <FooterTemplate>
                            <asp:Label ID="lblgvFmaxdur" runat="server"></asp:Label>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:Label ID="txtgvmaxdur" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxdur")).ToString("#,##0;-#,##0; ")%>'
                                Width="20px" Font-Size="11px" Style="text-align: right"></asp:Label>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual</br>Payment">

                        <FooterTemplate>
                            <asp:Label ID="lblgvFactamt" runat="server"></asp:Label>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvactamt" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actamt")).ToString("#,##0;-#,##0; ")%>'
                                Width="60px" Style="text-align: right"></asp:TextBox>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delay</br>Day">
                        <ItemTemplate>
                            <asp:Label ID="lblgvdday" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;-#,##0; ")%>'
                                Width="20px" Style="text-align: right"></asp:Label>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delay</br>in %">
                        <ItemTemplate>
                            <asp:Label ID="txtgvpercnt" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0;-#,##0; ")%>'
                                Width="20px" Style="text-align: right"></asp:Label>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason of Delay">

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvRemark" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks").ToString())%>'
                                Width="120px" Font-Size="10px"></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="+">

                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Comments" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>


                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="+">

                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDoc" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Documents" BackColor="Transparent" OnClick="lbtnDoc_Click"><span class="fa fa-upload" aria-hidden="true"></span></asp:LinkButton>


                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>




                </Columns>
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
                <FooterStyle CssClass="grvFooter" />
                <RowStyle CssClass="grvRows" />
            </asp:GridView>


        </div>
    </div>

    <%--Modal  --%>

    <div id="AddComments" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content  ">
                <div class="modal-header">


                    <h4 class="modal-title">
                        <span class="fa fa-table"></span>Add New Comments  </h4>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="row-fluid">
                        <asp:Label ID="lblactcode" runat="server" Visible="false"></asp:Label>

                        <div class="form-group" runat="server">
                            <label class="col-md-4">Comments</label>


                            <div class="col-md-10">
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" runat="server">
                            <label class="col-md-4">Previous Comments</label>
                            <asp:GridView ID="gvComm" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="150px" Style="margin-right: 0px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Date">

                                        <ItemTemplate>
                                            <asp:Label ID="txtAcStDate" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                Width="65px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Comments">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcomments" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comments").ToString())%>'
                                                Width="280px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvusername" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username").ToString())%>'
                                                Width="80px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>





                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>

                        </div>
                    </div>


                </div>
                <div class="modal-footer ">
                    <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>



                </div>
            </div>
        </div>
    </div>

    <div id="AddUser" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content  ">
                <div class="modal-header">


                    <h4 class="modal-title">
                        <span class="fa fa-table"></span>Add New User  </h4>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="row-fluid">
                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

                        <div class="form-group" runat="server">
                            <%--<label class="col-md-4">Comments</label>--%>


                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="chzn-select form-control inputTxt">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>


                </div>
                <div class="modal-footer ">
                    <asp:LinkButton ID="lbtnUpdateUsr" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnUpdateUsr_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>



                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="AddDoc" tabindex="-1" role="dialog" aria-labelledby="followingModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                    <h6 id="followingModalLabel" class="modal-title"><span class="fa fa-user-tag"></span>Upload more documents </h6>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body px-0" style="min-height: 140px;">

                    <div class="card-body">
                        <div id="dropzone" class="fileinput-dropzone">
                            <span>Drop files or click to upload.</span>
                            <!-- The file input field used as target for the file upload widget -->

                            <cc1:AsyncFileUpload runat="server"
                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                CompleteBackColor="White"
                                UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                OnUploadedComplete="FileUploadComplete" />
                        </div>
                        <div id="progress" class="progress progress-xs rounded-0 fade">
                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>

                        <asp:FileUpload ID="fileuploaddropzone" Visible="false" runat="server" />

                    </div>

                    <div class="modal-footer">
                         <asp:Label ID="lblImcode" runat="server" Visible="false"></asp:Label>

                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                    <div id="EmpAll" runat="server">

                                        <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("docurl") %>'></asp:Label>
                                         <asp:Label ID="lblcdate" Visible="false" runat="server" Text='<%# Eval("cdate") %>'></asp:Label>
                                               

                                        <a href="<%# ResolveUrl("~/"+Eval("docurl").ToString()) %>" class="uploadedimg" target="_blank">
                                            <asp:Image ID="GetImg" runat="server" Style="min-height: 70px;" CssClass="image img img-responsive img-thumbnail" />
                                            <div class="middle">
                                                <span><%# Eval("username") %></span>
                                            </div>
                                            <div class="checkboxcls">
                                                <asp:CheckBox ID="ChDel" runat="server" />
                                            </div>
                                        </a>



                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer">








                    <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>


                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



    <%--            
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>






