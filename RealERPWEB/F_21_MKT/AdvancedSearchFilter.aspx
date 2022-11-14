<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AdvancedSearchFilter.aspx.cs" Inherits="RealERPWEB.F_21_MKT.AdvancedSearchFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        }



    </script>

    <style type="text/css">
        .table th, .table td, card-header {
            padding: 4px;
        }
          .pnlSidebarCl {
            width: 80%;
            height: 100vh;
            position: absolute;
            right: 0;
        }

            .pnlSidebarCl .form-control {
                height: 25px;
                line-height: 25px;
                padding: 2px;
            }

        .divPnl table tr td, .divPnl table tr th {
            padding: 5px 5px;
        }
    </style>

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

            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row ml-2">

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <asp:Label ID="lblem" runat="server" CssClass="form-label">Associate Name</asp:Label>
                            <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label">Search Type</asp:Label>
                            <asp:DropDownList ID="ddlOther" runat="server" ClientIDMode="Static" CssClass="custom-select chzn-select">
                                <asp:ListItem Value="1">Prospect Name</asp:ListItem>
                                <asp:ListItem Value="2">PID</asp:ListItem>
                                <asp:ListItem Value="3">Phone</asp:ListItem>
                                <asp:ListItem Value="4">Email</asp:ListItem>
                                <asp:ListItem Value="5">NID</asp:ListItem>
                                <asp:ListItem Value="6">TIN</asp:ListItem>
                                <%--  <asp:ListItem Value="7">Prefered Area</asp:ListItem>
                                <asp:ListItem Value="8">Profission</asp:ListItem>--%>
                                <asp:ListItem Selected="True" Value="9">Choose Filter Key.........................</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label">Search </asp:Label>
                            <asp:TextBox ID="txtVal" runat="server" CssClass="form-control" TextMode="Search" autocomplete="off"></asp:TextBox>

                        </div>

                        <div class="col-md-1" style="margin-top: 22px">

                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-success" OnClick="lnkbtnOk_Click" AutoPostBack="True">Show</asp:LinkButton>

                        </div>

                    </div>

                </div>
                
            </div>

            <div class="card" style="background-color: whitesmoke; align-content: center">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-4">
                            <div class="card">
                                <div class="card-header bg-light"><span class="font-weight-bold text-muted">Employee Information</span></div>
                                <div class="card-body" runat="server" id="engst">
                                    <img src="~/../../../Upload/UserImages/3365001.png" style="display: block; margin-left: auto; margin-right: auto; width: 30%;" alt="User Image">
                                    <table class="table table-striped table-hober tblEMPinfo mt-2">
                                        <%--                <thead>
                                        <tr>
                                            <th></th>
                                            <th></th>

                                        </tr>
                                    </thead>--%>
                                        <tbody class="">
                                            <tr>
                                                <td class="font-weight-bold">PID</td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server">N/A</asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold ">Contact Person</td>
                                                <td>
                                                    <asp:Label ID="lblconper" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold">Primary Mobile</td>
                                                <td>
                                                    <asp:Label ID="lblmbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td class="font-weight-bold">Home Address</td>
                                                <td>
                                                    <asp:Label ID="lblhomead" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold">Profession</td>
                                                <td>
                                                    <asp:Label ID="lblprof" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="font-weight-bold">Status</td>
                                                <td>
                                                    <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>



                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="card mt-3 mb-3" id="pnlfollowup" runat="server">
                                <div class="card-header bg-light"><span class="font-weight-bold text-muted">Follow Up Summary</span></div>
                                <div class="card-body">
                                    <asp:Repeater ID="rpclientinfo" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <div class="col-md-12  col-lg-12 ">
                                                <div class="well">

                                                    <div class="col-sm-12 panel pt-3 b-3">

                                                        <div class=" col-sm-12">

                                                            <p>
                                                                <strong><%# DataBinder.Eval(Container, "DataItem.prosdesc")%></strong> <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %><br>




                                                                <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                                <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                                <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                                <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>

                                                                <asp:HiddenField ID="lblproscod" runat="server" />
                                                                <asp:HiddenField ID="lbleditempid" runat="server" />
                                                                <asp:HiddenField ID="lblgeneratedate" runat="server" />



                                                                <br>
                                                            </p>



                                                        </div>
                                                        <div class="row mb-5">
                                                            <div class="col-md-12">

                                                                <a href="#" class="btn btn-sm btn-primary mt-2">Re-schdule</a>
                                                                <a href="#" class="btn btn-sm btn-success mt-2">Delete</a>
                                                             
                                                                 <asp:LinkButton runat="server" type="button" class="btn  btn-success btn-sm mt-2" id="lbtntfollowup" data-target="#followup" OnClick="btnqclink_Click">FollowUp</asp:LinkButton>
                                                               
                                                                <a href="#" class="btn btn-sm btn-success mt-2">Addition</a>


                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>



                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>
                            
                                </div>


                            </div>
                         
                                 <div class="divPnl">
                           <div id="pnlSidebar" class="card pnlSidebarCl" runat="server" visible="false">
                        <div class="modal-content">
                            <div class="modal-header pt-0 pb-0 bg-light">
                                <h6 class="modal-title">Add FollowUp</h6>
                                <asp:LinkButton ID="pnlsidebarClose" OnClick="pnlsidebarClose_Click" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>
                            </div>
                            <div class="modal-body" id="followup">
                             
                                 <asp:GridView ID="gvInfo" runat="server" AllowPaging="false"
                                            AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass="table-condensed table-hover table-bordered grvContentarea">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNodis" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="displayhide">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCodedis" ClientIDMode="Static" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="lblgvTime" runat="server" BorderWidth="0" BackColor="Transparent" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtime")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcGrpdis" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1dis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgpdis" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                            Width="5px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvaldis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <FooterTemplate>

                                                        <asp:LinkButton ID="lbtnUpdateDiscussion" runat="server" OnClientClick="CloseModaldis();"  CssClass="btn  btn-success btn-xs ">Final Update</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <ItemTemplate>



                                                        <asp:TextBox ID="txtgvValdis" runat="server" BorderWidth="0" BackColor="Transparent" Font-Size="14px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>




                                                       

                                                        <asp:Panel ID="pnlTime" runat="server" Visible="false">
                                                            <asp:DropDownList ID="ddlhour" runat="server" CssClass="inputTxt ddlPage" Style="width: 50px; line-height: 22px;">
                                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                                <asp:ListItem Value="09" Selected="True">09</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>

                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlMmin" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                                <asp:ListItem Value="00">00</asp:ListItem>
                                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                <asp:ListItem Value="31">31</asp:ListItem>
                                                                <asp:ListItem Value="32">32</asp:ListItem>
                                                                <asp:ListItem Value="33">33</asp:ListItem>
                                                                <asp:ListItem Value="34">34</asp:ListItem>
                                                                <asp:ListItem Value="35">35</asp:ListItem>
                                                                <asp:ListItem Value="36">36</asp:ListItem>
                                                                <asp:ListItem Value="37">37</asp:ListItem>
                                                                <asp:ListItem Value="38">38</asp:ListItem>
                                                                <asp:ListItem Value="39">39</asp:ListItem>
                                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                                <asp:ListItem Value="41">41</asp:ListItem>
                                                                <asp:ListItem Value="42">42</asp:ListItem>
                                                                <asp:ListItem Value="43">43</asp:ListItem>
                                                                <asp:ListItem Value="44">44</asp:ListItem>
                                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                                <asp:ListItem Value="46">46</asp:ListItem>
                                                                <asp:ListItem Value="47">47</asp:ListItem>
                                                                <asp:ListItem Value="48">48</asp:ListItem>
                                                                <asp:ListItem Value="49">49</asp:ListItem>
                                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                                <asp:ListItem Value="51">51</asp:ListItem>
                                                                <asp:ListItem Value="52">52</asp:ListItem>
                                                                <asp:ListItem Value="53">53</asp:ListItem>
                                                                <asp:ListItem Value="54">54</asp:ListItem>
                                                                <asp:ListItem Value="55">55</asp:ListItem>
                                                                <asp:ListItem Value="56">56</asp:ListItem>
                                                                <asp:ListItem Value="57">57</asp:ListItem>
                                                                <asp:ListItem Value="58">58</asp:ListItem>
                                                                <asp:ListItem Value="59">59</asp:ListItem>


                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlslb" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                                                <asp:ListItem Value="PM">PM</asp:ListItem>




                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblschedulenumber" runat="server" BorderWidth="0" CssClass="btn btn-success btn-xs" Font-Size="14px"
                                                                Text="Schedule(0)"></asp:Label>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlStatus" runat="server" Visible="false">


                                                            <asp:CheckBoxList ID="ChkBoxLstStatus" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="form-control checkbox">
                                                            </asp:CheckBoxList>

                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlParic" runat="server" Visible="false">
                                                            <asp:ListBox ID="ddlPartic" runat="server" SelectionMode="Multiple" class="form-control chosen-select" Style="width: 300px !important;"
                                                                data-placeholder="Choose Person......" multiple="true"></asp:ListBox>

                                                        </asp:Panel>


                                                        <%-- <asp:Panel ID="Pnlcompany" runat="server">--%>
                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                            TabIndex="12">
                                                        </asp:DropDownList>
                                                        <%--</asp:Panel>--%>


                                                        <asp:Panel ID="PnlProject" runat="server">
                                                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                                TabIndex="12">
                                                            </asp:DropDownList>
                                                        </asp:Panel>
                                                        <asp:Panel ID="PnlUnit" runat="server">
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;"
                                                                TabIndex="12">
                                                            </asp:DropDownList>
                                                        </asp:Panel>


                                                        <asp:Panel ID="pnlVisit" runat="server" Visible="false">
                                                            <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;">
                                                            </asp:DropDownList>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlFollow" runat="server" Visible="false">
                                                            <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>



                                                            <asp:CheckBoxList ID="ChkBoxLstFollow" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="form-control checkbox">
                                                            </asp:CheckBoxList>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlLostResion" runat="server" Visible="false">
                                                            <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>


                                                            <asp:DropDownList ID="checkboxReson" Visible="false" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;">
                                                            </asp:DropDownList>


                                                        </asp:Panel>



                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle Width="700px" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>

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
